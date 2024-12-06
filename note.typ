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
		<Compile Include="../ngaq.Core/G.cs" />
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
