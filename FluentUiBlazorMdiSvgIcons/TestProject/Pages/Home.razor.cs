using FluentUiBlazorMdiSvgIcons;
using Microsoft.FluentUI.AspNetCore.Components;
using System.Collections.Immutable;
using System.Reflection;

namespace TestProject.Pages;
partial class Home
{
    private readonly ImmutableArray<PropertyInfo> allProperties = typeof(MdiSvg)
        .GetProperties()
        .ToImmutableArray();
    private readonly Random random = new Random();

    private Icon icon;
    private string name;
    protected override void OnParametersSet()
    {
        this.RandomIcon();
    }

    void RandomIcon()
    {
        var property = allProperties[random.Next(allProperties.Length)];
        this.icon = (Icon)property.GetValue(null);
        this.name = property.Name;
    }
}
