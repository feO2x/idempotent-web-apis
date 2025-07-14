using Light.SharedCore.DatabaseAccessAbstractions;
using Shared.Model;

namespace ServiceB.Contacts.CreateContact;

public interface ICreateContactSession : ISession
{
    void AddContact(Contact contact);
}
