using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RRExpress.Common {

    public abstract class TreeNode { }

    public abstract class TreeNode<TNative, TTreeNode, TID> : TreeNode where TTreeNode : TreeNode {

        public TNative Parent { get; set; }

        public TID PID { get; set; }

        public TID ID { get; set; }

        public TNative Data { get; set; }

        public IEnumerable<TTreeNode> Subs { get; set; }
    }

    public static class TreeNodeHelper {


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TNative">原始类型</typeparam>
        /// <typeparam name="TTreeNode">包装后的TreeNode类型</typeparam>
        /// <typeparam name="TID">ID类型</typeparam>
        /// <param name="datas">数据数据</param>
        /// <param name="parentIDGetter"></param>
        /// <param name="idGetter"></param>
        /// <param name="pid">父ID</param>
        /// <returns></returns>
        public static IEnumerable<TTreeNode>
            BuildTree<TNative, TTreeNode, TID>(
                    this IEnumerable<TNative> datas,
                    Func<TNative, TID> parentIDGetter,
                    Func<TNative, TID> idGetter,
                    TID pid

                ) where TTreeNode : TreeNode<TNative, TTreeNode, TID>, new() {

            if (datas == null)
                return null;

            return datas.Where(d => parentIDGetter.Invoke(d).Equals(pid))
                    .Select(d => new TTreeNode() {
                        Data = d,
                        ID = idGetter.Invoke(d),
                        PID = pid,
                        Subs = datas.BuildTree<TNative, TTreeNode, TID>(parentIDGetter, idGetter, idGetter.Invoke(d)),
                        Parent = datas.FirstOrDefault(dd => idGetter.Invoke(d).Equals(pid))
                    });
        }

        private static void TT() {
            IEnumerable<Test> datas = null;
            var a = datas.BuildTree<Test, TestTreeNode, int>(t => t.PID, t => t.ID, 0);
            var b = a.First().IsSelected;
        }
    }

    class Test {
        public int ID { get; set; }

        public int PID { get; set; }

        public string Name { get; set; }
    }

    class TestTreeNode : TreeNode<Test, TestTreeNode, int> {
        public bool IsSelected { get; set; }
    }
}
