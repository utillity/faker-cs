﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Faker.Extensions;

namespace Faker
{
    public static class Internet
    {
        private static readonly IEnumerable<Func<string>> UserNameFormats = new List<Func<string>>
        {
            () => Name.First().AlphanumericOnly().ToLowerInvariant(),
            () => $"{Name.First().AlphanumericOnly()}{new[] {".", "_"}.Random()}{Name.Last().AlphanumericOnly()}"
                .ToLowerInvariant()
        };

        public static string Email()
        {
            return $"{UserName()}@{DomainName()}";
        }

        public static string Email(string name)
        {
            return $"{UserName(name)}@{DomainName()}";
        }

        public static string FreeEmail()
        {
            return $"{UserName()}@{Resources.Internet.FreeMail.Split(Config.Separator).Random()}";
        }

        public static string UserName()
        {
            return UserNameFormats.Random();
        }

        public static string UserName(string name)
        {
            return Regex.Replace(name, @"[^\w]+", x => new[] {".", "_"}.Random(), RegexOptions.Compiled)
                .ToLowerInvariant();
        }

        public static string DomainName()
        {
            return $"{DomainWord()}.{DomainSuffix()}";
        }

        public static string Url()
        {
            var subDomain = SubDomain();
            var page = Page();
            return $"http://www.{DomainName()}/{subDomain}/{page}.html";
        }

        public static string SecureUrl()
        {
            var subDomain = SubDomain();
            var page = Page();
            return $"https://www.{DomainName()}/{subDomain}/{page}.html";
        }

        public static string DomainWord()
        {
            return Company.Name().Split(' ').First().AlphanumericOnly().ToLowerInvariant();
        }

        public static string DomainSuffix()
        {
            return Resources.Internet.DomainSuffix.Split(Config.Separator).Random();
        }

        private static string SubDomain()
        {
            return string.Join("",
                Enumerable.Range(1, RandomNumber.Next(5, 12))
                    .Select(x => Resources.Identification.Alphabet.Split(Config.Separator).Random().ToLower()));
        }

        private static string Page()
        {
            return string.Join("",
                Enumerable.Range(1, RandomNumber.Next(3, 8))
                    .Select(x => Resources.Identification.Alphabet.Split(Config.Separator).Random().ToLower()));
        }
    }
}