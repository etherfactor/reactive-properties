using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace EtherGizmos.ReactiveProperties.Generators;

/// <summary>
/// Finds candidate classes for <see cref="ReactivePropertyGenerator"/>.
/// </summary>
public class ReactivePropertySyntaxReceiver : ISyntaxReceiver
{
    public List<ClassDeclarationSyntax> CandidateClasses { get; } = new List<ClassDeclarationSyntax>();

    /// <inheritdoc/>
    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        //Class is a candidate if it is partial with generic fields
        if (syntaxNode is ClassDeclarationSyntax classDeclaration &&
            classDeclaration.Modifiers.Any(m => m.IsKind(SyntaxKind.PartialKeyword)) &&
            classDeclaration.Members.OfType<FieldDeclarationSyntax>().Any(field =>
                field.Declaration.Type is GenericNameSyntax genericNameSyntax))
        {
            CandidateClasses.Add(classDeclaration);
        }
    }
}
