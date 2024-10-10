using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using model;
using model.consts;

namespace db;

public class NgaqDbCtx : DbContext
{
	public DbSet<KV> KVEntities { get; set; }

	protected override void OnModelCreating(ModelBuilder mb){
		base.OnModelCreating(mb);
		// 這裡可以進行進一步的配置，例如設置主鍵、索引等

//DatabaseGenerated(DatabaseGeneratedOption.Identity)
		mb.Entity<KV>().HasKey(e=>e.id);
		mb.Entity<KV>().Property(e=>e.id).ValueGeneratedOnAdd();
		
		mb.Entity<KV>().HasIndex(e => e.bl);
		mb.Entity<KV>().HasIndex(e => e.ct);
		mb.Entity<KV>().HasIndex(e => e.ut);
		mb.Entity<KV>().HasIndex(e => e.kStr);
		mb.Entity<KV>().HasIndex(e => e.kI64);
		mb.Entity<KV>().HasIndex(e => e.kDesc);

		mb.Entity<KV>().Property(e=>e.kType).HasDefaultValue(KVType.STR.ToString());
		mb.Entity<KV>().Property(e=>e.vType).HasDefaultValue(KVType.STR.ToString());

		

		mb.Entity<KV>().Property(e=>e.ct).HasDefaultValueSql("(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))");
		mb.Entity<KV>().Property(e=>e.ut).HasDefaultValueSql("(strftime('%s', 'now') || substr(strftime('%f', 'now'), 4))");

		//var N = (n)=>{return nameof(N)};

	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
		// 在這裡配置您的數據庫連接字符串
		var dir = G.getBaseDir()+"/"+G.main;
		var path = dir+"/db/db.sqlite";
		std.IO.Directory.CreateDirectory(dir); // TODO不效
		optionsBuilder.UseSqlite($"Data Source={path}");
	}

	public Task<IDbContextTransaction> BeginTrans(){
		return Database.BeginTransactionAsync();
	}
}

//dotnet ef migrations add InitialCreate
//dotnet ef database update


/* 
ts的class-transformer庫是直接把轉換邏輯函數寫進裝飾器裏的
如
class User {
	@Transform(value => moment(value))
	time:Moment
}

c#的映射庫是怎麼做的?
*/

//AutoMapper:

/* 
using AutoMapper;

public class User
{
	public DateTime Time { get; set; }
}

public class UserDto
{
	public string Time { get; set; }
}

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<User, UserDto>()
			.ForMember(
				dest => dest.Time
				, opt => opt.MapFrom(src => src.Time.ToString("o"))
			); // ISO 8601 格式
	}
}

// 使用示例
var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
var mapper = config.CreateMapper();

var user = new User { Time = DateTime.Now };
var userDto = mapper.Map<UserDto>(user);

*/


/* 
using Newtonsoft.Json;
using System;

public class User
{
	[JsonConverter(typeof(CustomDateTimeConverter))]
	public DateTime Time { get; set; }
}

public class CustomDateTimeConverter : JsonConverter
{
	public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
	{
		var dateTime = (DateTime)value;
		writer.WriteValue(dateTime.ToString("o")); // ISO 8601 格式
	}

	public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
	{
		var dateTimeString = (string)reader.Value;
		return DateTime.Parse(dateTimeString);
	}

	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(DateTime);
	}
}

// 使用示例
var user = new User { Time = DateTime.Now };
var json = JsonConvert.SerializeObject(user);
var deserializedUser = JsonConvert.DeserializeObject<User>(json);

*/