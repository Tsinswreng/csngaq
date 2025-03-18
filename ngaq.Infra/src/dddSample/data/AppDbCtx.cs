using System.Reflection;
using Ardalis.SharedKernel;
using Microsoft.EntityFrameworkCore;
using ngaq.Core.dddSample.contributorAgg;
using ngaq.Infra.dddSample.data;

public class AppDbCtx(
	DbContextOptions<AppDbCtx> options
	,IDomainEventDispatcher? dispatcher
)
	:DbContext(options)
{
	protected readonly IDomainEventDispatcher? _dispatcher = dispatcher;

	public DbSet<Contributor> contributors => Set<Contributor>();//getter?

	protected override void OnModelCreating(ModelBuilder modelBuilder) {
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());//?
	}

	public override async Task<int> SaveChangesAsync(
		CancellationToken ct=default
	){
		var result = await base.SaveChangesAsync(ct).ConfigureAwait(false);//?
		// ignore events if no dispatcher provided
		if(_dispatcher == null){
			return result;
		}

		// dispatch events only if save was successful
		var entitiesWithEvents = ChangeTracker.Entries<HasDomainEventsBase>()
			.Select(e=>e.Entity)
			.Where(e=>e.DomainEvents.Any())
			.ToArray()
		;
		await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
		return result;
	}
}