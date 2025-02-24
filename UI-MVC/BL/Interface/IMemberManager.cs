using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_MVC.Domain;
using UI_MVC.Models;

namespace UI_MVC.BL.Interface
{
    interface IMemberManager
    {
        void addMember(string lastname, string firstname, string country, string address, string housenumber, int? mailbox, int zip, string city, string phone, string email, DateTime birthdate, Gender gender, int? CMASNum, int? LIFRASNum, string AllergiesOrMedicaments);
        IEnumerable<Member> GetMembers();
        void ChangeMember(Member member);
        Member GetMember(int memberId);
        Member GetFullMember(int memberId);
        IEnumerable<Member> GetFullMembers();
        IEnumerable<Member> GetMembersWithEcg();
        IEnumerable<Member> GetMembersWithSubscription();
        IEnumerable<Member> GetMembersWithCFPS();
        List<Member> GetExportToExcel(int year);
        void RemoveFunction(int FunctionId);
    }
}