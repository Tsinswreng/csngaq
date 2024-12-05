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
