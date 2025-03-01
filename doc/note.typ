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


= 自定義可傳參控件
[2025-02-10T18:41:25.726+08:00_W7-1]

`E:\_code\csngaq\ngaq.UI\Cmpnt\ScrollInput\ScrollInput.axaml.nc.xml`:

```xml
<UserControl xmlns="https://github.com/avaloniaui"
		xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
		xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
		xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
		mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
		xmlns:vm="clr-namespace:ngaq.UI.Cmpnt.ScrollInput"
		x:Class="ngaq.UI.Cmpnt.ScrollInput.ScrollInput"
		x:DataType="vm:ScrollInputVm"
		x:Name="RootName"
>
	<Design.DataContext>

	</Design.DataContext>


	<StackPanel Orientation="Horizontal">
		<ScrollViewer VerticalScrollBarVisibility="Auto"
						HorizontalScrollBarVisibility="Auto">
			<TextBox AcceptsReturn="True"
						Height="{Binding Height, ElementName=RootName}"
						MinHeight="{Binding MinHeight, ElementName=RootName}"
						MaxHeight="{Binding MaxHeight, ElementName=RootName}"
						Width="{Binding Width, ElementName=RootName}"
						MinWidth="{Binding MinWidth, ElementName=RootName}"
						MaxWidth="{Binding MaxWidth, ElementName=RootName}"
						Padding="{Binding Padding, ElementName=RootName}"
						Margin="{Binding Margin, ElementName=RootName}"
						TextWrapping="Wrap"
						VerticalAlignment="Stretch"
						HorizontalAlignment="Stretch"
						CornerRadius="0"
						`Text="{Binding Text, RelativeSource={RelativeSource AncestorType=UserControl}}" `報錯 找不到Text
						`Text="{Binding Text}" `忘了
						Text="{Binding Text, ElementName=RootName, Mode=TwoWay}"
						`Text="{Binding Text, ElementName=RootName}" `變化ˋ能從代碼傳至UI、反㞢則未試
						`Text="123" `只顯示123不變
			/>
		</ScrollViewer>
	</StackPanel>
</UserControl>
```

;

`E:\_code\csngaq\ngaq.UI\Cmpnt\ScrollInput\ScrollInput.axaml.cs`:
```cs
using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Metadata;


namespace ngaq.UI.Cmpnt.ScrollInput;
//global using str = System.String;
public partial class ScrollInput : UserControl{
	public ScrollInput(){
		InitializeComponent();
	}

	public static readonly StyledProperty<str> TextProperty
		= AvaloniaProperty.Register<ScrollInput, str>(nameof(Text));

	//[Content]
	public str Text{
		get{return GetValue(TextProperty);}
		set{SetValue(TextProperty, value);}
	}

	private void InitializeComponent(){
		AvaloniaXamlLoader.Load(this);
	}

}

```

;

ʃˋ調用處:
```xml
<UserControl xmlns="https://github.com/avaloniaui"
		xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
		xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
		xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
		mc:Ignorable="d" d:DesignWidth="800" d:DesignHeight="450"
		xmlns:vm="clr-namespace:ngaq.UI.ViewModels"
		xmlns:ScrollInput="clr-namespace:ngaq.UI.Cmpnt.ScrollInput"
		x:Class="ngaq.UI.Views.KV.KvView"
		x:DataType="vm:KV.KvVM"
>

<ScrollInput:ScrollInput Text="{Binding ct, Mode=TwoWay}"/>
`要寫Mode=TwoWay 、不寫則默認單向綁定。
`此異於<TextBox Text="{Binding ct}">之默認潙Mode=TwoWay.
</UserControl>
```



= UserControl..PointerPressed 不可用
[2025-02-28T20:09:26.863+08:00_W9-5]


=
[2025-02-28T20:30:40.824+08:00_W9-5]
Button之Content中 若以SelectableTextBlock代TextBlock則影響Button之判定

=
[2025-02-28T20:35:14.046+08:00_W9-5]
Button亦可設Border


=
[2025-03-01T14:02:25.617+08:00_W9-6]
DataGrid 似不支持 自動生成列 誧 類芝不在同一程序集者。



=
[2025-03-01T20:29:32.883+08:00_W9-6]
縱向之WrapPanel不支持 子元素與父容器 同寬