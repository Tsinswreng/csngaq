using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ngaq.Core.model;
using model.consts;

namespace db;

public class NgaqDbCtx : DbContext
{
	public DbSet<WordKv> WordKV { get; set; } = null!;
	public DbSet<_KV> _KV{get;set;} = null!;

	protected code _configKV<T>(ModelBuilder mb) where T : class, I_KV {

		mb.Entity<T>().HasIndex(e => e.kStr);
		mb.Entity<T>().HasIndex(e => e.kI64);
		mb.Entity<T>().HasIndex(e => e.kDesc);

		mb.Entity<T>().Property(e=>e.kType).HasDefaultValue(KVType.STR.ToString());
		mb.Entity<T>().Property(e=>e.vType).HasDefaultValue(KVType.STR.ToString());
		return 0;
	}

	protected code _configIdBlCtUt<T>(ModelBuilder mb) where T : class, I_RowBaseInfo {
		mb.Entity<T>().HasKey(e=>e.id);
		mb.Entity<T>().Property(e=>e.id).ValueGeneratedOnAdd();
		mb.Entity<T>().HasIndex(e => e.bl);
		mb.Entity<T>().HasIndex(e => e.ct);
		mb.Entity<T>().HasIndex(e => e.ut);
		mb.Entity<T>().Property(e=>e.ct).HasDefaultValueSql("(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))");
		mb.Entity<T>().Property(e=>e.ut).HasDefaultValueSql("(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))");
		return 0;
	}

	protected override void OnModelCreating(ModelBuilder mb){
		base.OnModelCreating(mb);
		mb.Entity<WordKv>().ToTable(nameof(WordKV)); //public class WordKV : KV{}、mb.Entity<WordKV>().ToTable(nameof(WordKV));不可、子類無物
		_configIdBlCtUt<WordKv>(mb);
		_configKV<WordKv>(mb);

		mb.Entity<_KV>().ToTable(nameof(_KV));
		_configIdBlCtUt<_KV>(mb);
		_configKV<_KV>(mb);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
		// 在這裡配置您的數據庫連接字符串
		var dir = G.getBaseDir()+"/../db";
		//TODO 使同exe之目錄
		var path = dir+"/csngaq.sqlite";
		//G.log(path);//t
		std.IO.Directory.CreateDirectory(dir); // TODO不效
		optionsBuilder.UseSqlite($"Data Source={path}");
	}

	public Task<IDbContextTransaction> BeginTrans(){
		return Database.BeginTransactionAsync();
	}
}

//dotnet ef migrations add InitialCreate
//dotnet ef database update

//dotnet ef migrations add _20241206230213_csngaq_init
//dotnet ef migrations add _20250125175056_AddCol_status

