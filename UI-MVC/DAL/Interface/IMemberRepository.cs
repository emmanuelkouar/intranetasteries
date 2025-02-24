using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UI_MVC.Domain;
using UI_MVC.Models;

namespace UI_MVC.DAL.Interface
{
    interface IMemberRepository
    {
        void CreateMember(Member member);
        IEnumerable<Member> ReadMembers();
        void UpdateMember(Member member);
        Member ReadMember(int memberId);
        Member ReadFullMember(int memberId);
        IEnumerable<Member> ReadFullMembers();
        IEnumerable<Member> ReadMembersWithECG();
        IEnumerable<Member> ReadMembersWithSubscription();
        IEnumerable<Member> ReadMembersWithCFPS();
        List<Member> ReadExportToExcel(int year);
        void DeleteFunction(int FunctionId);
    }
}
