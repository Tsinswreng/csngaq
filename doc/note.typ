= Resources.Design.cs不生成
[2024-12-03T12:39:38.544+08:00_W49-2]

#image("assets/2024-12-03-12-39-32.png")

AI曰亦可
```bash
resgen MyResources.resx /str:cs,MyNamespace,MyResources,MyResources.cs
```


=
[2024-12-05T14:18:17.206+08:00_W49-4]
ngaq.csproj
==
```xml

	<ItemGroup>
		<ProjectReference Include="../ngaq.Core/ngaq.Core.csproj" />
		<Compile Include="../ngaq.Core/GlobalUsing.cs" />
	</ItemGroup>


<ItemGroup>
  <EmbeddedResource Update="Assets/Resources.resx">
    <Generator>PublicResXFileCodeGenerator</Generator>
    <LastGenOutput>Resources.Designer.cs</LastGenOutput>
  </EmbeddedResource>
</ItemGroup>

    <ItemGroup>
      <Compile Update="Assets/Resources.Designer.cs">
        <DesignTime>True</DesignTime>
        <AutoGen>True</AutoGen>
        <DependentUpon>Resources.resx</DependentUpon>
      </Compile>
    </ItemGroup>
```


=
[2024-12-05T18:16:01.712+08:00_W49-4]
```bash
dotnet new sln -n ngaq
dotnet sln add ./ngaq.Core/ngaq.Core.csproj
dotnet sln add ./ngaq/ngaq.csproj
dotnet sln add ./ngaq.Desktop/ngaq.Desktop.csproj
dotnet sln add ./ngaq.Browser/ngaq.Browser.csproj
```


=
[2024-12-06T00:03:05.340+08:00_W49-5]
c\#根目錄下已有.sln、c\# 無報錯提示、只有代碼跳轉功能
== 解:
假設.sln文件正確無誤

先試亂敲、耐心等待足夠久ʹ時、觀有無報錯提示

若無、則新開文件夾、試`dotnet new avalonia.xplat -o xxx`、亂敲、觀LSP工作正常否

若彼無病、唯己ʹ項目無、則全遷己ʹ項目內容至他ʹ文件夾、然後先試遷回一個、生成.sln、亂敲造錯、靜候、觀有無報錯提示

```bash
mv ./* ../temp/ # 遷移項目內容至他處
#先試遷其一回原處
mv ../temp/ngaq.Core
dotnet new sln -n ngaq
dotnet sln add ./ngaq.Core.csproj
```

無誤則可盡遷回。


=
[2024-12-06T22:47:29.261+08:00_W49-5]

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




= efcore 多子類配置法
[2024-12-06T23:05:13.391+08:00_W49-5]

父類爲KV、子類爲WordKV、`_KV`
```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using ngaq.Core.model;
using model.consts;

namespace db;

public class NgaqDbCtx : DbContext
{
	public DbSet<WordKV> WordKV { get; set; }
	public DbSet<_KV> _KV{get;set;} //no usage

	protected code _configKV<T>(ModelBuilder mb) where T : class, I_KV {

		mb.Entity<T>().HasIndex(e => e.kStr);
		mb.Entity<T>().HasIndex(e => e.kI64);
		mb.Entity<T>().HasIndex(e => e.kDesc);

		mb.Entity<T>().Property(e=>e.kType).HasDefaultValue(KVType.STR.ToString());
		mb.Entity<T>().Property(e=>e.vType).HasDefaultValue(KVType.STR.ToString());
		return 0;
	}

	protected code _configIdBlCtUt<T>(ModelBuilder mb) where T : class, I_IdBlCtUt {
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
		mb.Entity<WordKV>().ToTable(nameof(WordKV)); //public class WordKV : KV{}、mb.Entity<WordKV>().ToTable(nameof(WordKV));不可、子類無物
		_configIdBlCtUt<WordKV>(mb);
		_configKV<WordKV>(mb);

		mb.Entity<_KV>().ToTable(nameof(_KV));
		_configIdBlCtUt<_KV>(mb);
		_configKV<_KV>(mb);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
		// 在這裡配置您的數據庫連接字符串
		var dir = G.getBaseDir()+"/db";
		var path = dir+"/csngaq.sqlite";
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



```



