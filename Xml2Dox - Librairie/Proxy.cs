﻿using System.Reflection;

namespace Xml2Dox.Librairie;

/// <summary>
/// A class created in order to put in common all the call by the two applications (Console and IHM)
/// </summary>
/// <remarks>This class try to respect the design pattern proxy</remarks>
public class Proxy : IXmlDoc
{
    private IEnumerable<Type> listParseurs;
    private static readonly object locker = new();
    private static Proxy instance = null!;

    private Proxy() 
    {
        listParseurs = Assembly.GetAssembly(typeof(IXmlDoc))!.GetTypes().Where(t => t.IsClass);
    }

    /// <summary>
    /// 
    /// </summary>
    public static Proxy Instance
    {
        get
        {
            if(instance is null)
            {
                lock(locker)
                {
                    instance ??= new Proxy();
                }
            }
            return instance;
        }
    }

    /// <inheritdoc/>
    public bool GenerateDocumentation(XsltDocOptions options)
    {
        var parseur = (IXmlDoc)Activator.CreateInstance(listParseurs.First(t => t.Name == $"XmlDocTo{options.Format}"))!;
        if(parseur is null)
            return false;
        return parseur.GenerateDocumentation(options);
    }
}