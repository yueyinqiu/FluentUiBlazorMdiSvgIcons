using Microsoft.CodeAnalysis.CSharp;
using System.Diagnostics;
using System.Text;

var rootPath = "../../../../../";
var svgPath = $"{rootPath}MaterialDesign-SVG/svg/";
var projectPath = $"{rootPath}FluentUiBlazorMdiSvgIcons/FluentUiBlazorMdiSvgIcons/";

var svgFiles = Directory.GetFiles(svgPath);

Trace.Assert(svgFiles.Length > 100);
Trace.Assert(new DirectoryInfo(projectPath).EnumerateFiles().Any(
    x => x.Name is "FluentUiBlazorMdiSvgIcons.csproj"));

using var output = new StreamWriter($"{projectPath}MdiSvg.cs", false, Encoding.UTF8);
output.WriteLine("using Microsoft.FluentUI.AspNetCore.Components;");
output.WriteLine("namespace FluentUiBlazorMdiSvgIcons;");
output.WriteLine();
output.WriteLine("public static class MdiSvg");
output.WriteLine("{");

Array.Sort(svgFiles);
foreach(var file in svgFiles)
{
    Console.WriteLine(file);

    Trace.Assert(file.EndsWith(".svg"));
    var fileName = Path.GetFileNameWithoutExtension(file);
    var fileNameSplit = fileName.Split("-");
    var fileNameBuilder = new StringBuilder();
    foreach(var s in fileNameSplit)
        _ = fileNameBuilder.Append(char.ToUpperInvariant(s[0])).Append(s[1..]);
    fileName = fileNameBuilder.ToString();

    var path = File.ReadAllText(file);
    Trace.Assert(path.EndsWith("</svg>"));
    path = path[..^("</svg>").Length];
    var pathSplit = path.Split(" viewBox=\"0 0 24 24\">");
    Trace.Assert(pathSplit.Length is 2);
    path = pathSplit[1];
    path = SymbolDisplay.FormatLiteral(path, true);

    output.WriteLine($"    public static Icon {fileName} => new(\"{fileName}\", IconVariant.Regular, IconSize.Size24, {path});");
}

output.WriteLine("}");