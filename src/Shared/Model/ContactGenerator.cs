using System.Collections.Generic;
using Bogus;

namespace Shared.Model;

public static class ContactGenerator
{
    private static readonly Faker<Contact> ContactFaker = new Faker<Contact>()
       .UseSeed(42)
       .StrictMode(ensureRulesForAllProperties: true)
       .RuleFor(x => x.Id, _ => 0)
       .RuleFor(x => x.Name, f => f.Person.FullName)
       .RuleFor(x => x.Email, f => f.Random.Bool() ? f.Person.Email : null)
       .RuleFor(x => x.PhoneNumber, f => f.Random.Bool() ? f.Person.Phone : null);

    public static Contact Generate() => ContactFaker.Generate();

    public static List<Contact> Generate(int count) => ContactFaker.Generate(count);
}