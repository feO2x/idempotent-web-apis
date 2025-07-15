using Light.SharedCore.DatabaseAccessAbstractions;
using Shared.Contacts;

namespace ServiceB.Contacts.CreateContact;

public interface ICreateContactSession : ISession
{
    void AddContact(Contact contact);
}
