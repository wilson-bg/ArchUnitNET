﻿//  Copyright 2019 Florian Gather <florian.gather@tngtech.com>
// 	Copyright 2019 Paula Ruiz <paularuiz22@gmail.com>
// 	Copyright 2019 Fritz Brandhuber <fritz.brandhuber@tngtech.com>
// 
// 	SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.Linq;
using ArchUnitNET.Domain.Dependencies;
using ArchUnitNET.Loader;

namespace ArchUnitNET.Domain
{
    public class MethodMember : IMember
    {
        private readonly TypeInstance<IType> _returnTypeInstance;

        public MethodMember(string name, string fullName, IType declaringType, Visibility visibility,
            TypeInstance<IType> returnType, bool isVirtual, MethodForm methodForm, bool isGeneric, bool isStub,
            bool isCompilerGenerated)
        {
            Name = name;
            FullName = fullName;
            DeclaringType = declaringType;
            Visibility = visibility;
            _returnTypeInstance = returnType;
            IsVirtual = isVirtual;
            MethodForm = methodForm;
            IsGeneric = isGeneric;
            IsStub = isStub;
            IsCompilerGenerated = isCompilerGenerated;
        }

        public MethodMember(string name, string fullName, IType declaringType, Visibility visibility,
            TypeInstance<IType> returnType, bool isVirtual,
            MethodForm methodForm, bool isGeneric, bool isStub, bool isCompilerGenerated,
            List<TypeInstance<IType>> parameters)
        {
            Name = name;
            FullName = fullName;
            DeclaringType = declaringType;
            Visibility = visibility;
            ParameterInstances = parameters;
            _returnTypeInstance = returnType;
            IsVirtual = isVirtual;
            MethodForm = methodForm;
            IsGeneric = isGeneric;
            IsStub = isStub;
            IsCompilerGenerated = isCompilerGenerated;
        }

        public bool IsVirtual { get; }
        public MethodForm MethodForm { get; }

        internal List<TypeInstance<IType>> ParameterInstances { get; } = new List<TypeInstance<IType>>();
        public IEnumerable<IType> Parameters => ParameterInstances.Select(instance => instance.Type);
        public IType ReturnType => _returnTypeInstance.Type;
        public bool IsStub { get; }
        public bool IsCompilerGenerated { get; }
        public bool IsGeneric { get; }
        public List<GenericParameter> GenericParameters { get; } = new List<GenericParameter>();
        public Visibility Visibility { get; }
        public List<Attribute> Attributes { get; } = new List<Attribute>();
        public List<IMemberTypeDependency> MemberDependencies { get; } = new List<IMemberTypeDependency>();
        public List<IMemberTypeDependency> MemberBackwardsDependencies { get; } = new List<IMemberTypeDependency>();
        public List<ITypeDependency> Dependencies => MemberDependencies.Cast<ITypeDependency>().ToList();

        public List<ITypeDependency> BackwardsDependencies =>
            MemberBackwardsDependencies.Cast<ITypeDependency>().ToList();

        public string Name { get; }
        public string FullName { get; }
        public IType DeclaringType { get; }

        public override string ToString()
        {
            return $"{DeclaringType.FullName}::{Name}";
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((MethodMember) obj);
        }

        private bool Equals(MethodMember other)
        {
            return FullName.Equals(other.FullName);
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode();
        }
    }
}