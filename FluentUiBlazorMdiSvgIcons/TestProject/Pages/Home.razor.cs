using FluentUiBlazorMdiSvgIcons;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Extensions;
using System;
using System.Collections.Immutable;
using System.Reflection;

namespace TestProject.Pages;
partial class Home
{
    private ImmutableArray<MethodInfo> allMethods = typeof(MdiSvg).GetMethods().ToImmutableArray();
    private ImmutableArray<IconVariant> allVariants = [
        IconVariant.Regular, 
        IconVariant.Light, 
        IconVariant.Filled
    ];
    private ImmutableArray<IconSize> allSizes = [
        IconSize.Size10,
        IconSize.Size12,
        IconSize.Size16,
        IconSize.Size20,
        IconSize.Size24,
        IconSize.Size28,
        IconSize.Size32,
        IconSize.Size48 ,
    ];
    private Random random = new Random();

    private Icon icon;
    private string parameters;
    protected override void OnParametersSet()
    {
        this.RandomIcon();
    }

    void RandomIcon()
    {
        var method = allMethods[random.Next(allMethods.Length)];
        var variant = allVariants[random.Next(allVariants.Length)];
        var size = allSizes[random.Next(allSizes.Length)];

        this.icon = (Icon)method.Invoke(null, [variant, size]);
        this.parameters = $"{method.Name} ({variant.GetDisplayName()}, {size.GetDisplayName()})";
    }
}
