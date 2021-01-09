using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Diagnostics.CodeAnalysis;

namespace UPD.EntityFramework
{
    public static class Extenstions
    {
        public static PropertyBuilder<TProperty> HasDefaultCurrentDate<TProperty>([NotNullAttribute] this PropertyBuilder<TProperty> propertyBuilder)
            where TProperty : IComparable<DateTime>
        {
            propertyBuilder.HasDefaultValueSql<TProperty>("getdate()");
            return propertyBuilder;
        }
    }
}
