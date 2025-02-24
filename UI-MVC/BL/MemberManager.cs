using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UI_MVC.BL.Interface;
using UI_MVC.DAL;
using UI_MVC.DAL.Interface;
using UI_MVC.Domain;
using UI_MVC.Models;

namespace UI_MVC.BL
{
    public class MemberManager : IMemberManager
    {
        private IMemberRepository repo = new MemberRepository();
        public void addMember(string lastname, string firstname, string country, string address, string housenumber, int? mailbox, int zip, string city, string phone, string email, DateTime birthdate, Gender gender, int? CMASNum, int? LIFRASNum, string AllergiesOrMedicaments)
        {
            Member member = new Member()
            {
                LastName = lastname,
                FirstName = firstname, 
                Country = country, 
                Address = address, 
                HouseNumber = housenumber, 
                MailBox = mailbox, 
                Zip = zip, 
                City = city, 
                Phone = phone, 
                Email = email, 
                Birthdate = birthdate, 
                Gender = gender,
                CMASNumber = CMASNum,
                LIFRASNumber = LIFRASNum,
                AllergiesOrMedicaments = AllergiesOrMedicaments
            };

            repo.CreateMember(member);
        }

        public void ChangeMember(Member member)
        {
            repo.UpdateMember(member);
        }

        public Member GetMember(int memberId)
        {
            return repo.ReadMember(memberId);
        }

        public IEnumerable<Member> GetMembers()
        {
            return repo.ReadMembers();
        }

        public IEnumerable<Member> GetFullMembers()
        {
            return repo.ReadFullMembers();
        }

        public Member GetFullMember(int memberId)
        {
            return repo.ReadFullMember(memberId);
        }

        public IEnumerable<Member> GetMembersWithEcg()
        {
            return repo.ReadMembersWithECG();
        }

        public IEnumerable<Member> GetMembersWithSubscription()
        {
            return repo.ReadMembersWithSubscription();
        }

        public IEnumerable<Member> GetMembersWithCFPS()
        {
            return repo.ReadMembersWithCFPS();
        }

        public void RemoveFunction(int FunctionId)
        {
            repo.DeleteFunction(FunctionId);
        }

        public List<Member> GetExportToExcel(int year)
        {
            return repo.ReadExportToExcel(year);
        }
    }
}