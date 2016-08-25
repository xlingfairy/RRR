﻿using RRExpress.AppCommon;
using AsNum.XFControls;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using AsNum.XFControls.Services;

namespace RRExpress {
    /// <summary>
    /// 订单列表基类
    /// </summary>
    public abstract class ListBase : BaseVM, ISelectable {
        public bool IsSelected { get; set; }

        public ICommand SelectedCommand { get; set; }

        public ICommand UnSelectedCommand {
            get; set;
        }


        public ICommand RefreshCmd { get; }

        public ICommand LoadMoreCmd { get; }

        public BindableCollection<object> Datas {
            get; set;
        }

        /// <summary>
        /// Item1: 是否有错误， Item2: 结果集
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        protected abstract Task<Tuple<bool, IEnumerable<object>>> GetDatas(int page);

        private bool HasFirstLoaded = false;
        private int NextPage = 0;


        public ListBase() {
            this.SelectedCommand = new Command(async () => {
                if (!this.HasFirstLoaded) {
                    this.HasFirstLoaded = true;

                    await Task.Delay(500).ContinueWith(async t => {
                        await this.LoadData(true);
                    });
                }
            });

            this.Datas = new BindableCollection<object>();

            this.LoadMoreCmd = new Command(async () => {
                await this.LoadData();
            });

            this.RefreshCmd = new Command(async () => {
                await this.LoadData(true);
            });
        }

        protected async Task LoadData(bool isReload = false) {
            //if (this.IsBusy) {
            //    //ListView.IsRefreshing 绑定到这个属性上, 造成双向绑定,所以, 不能用它作为判断
            //    //return;
            //}

            var toast = DependencyService.Get<IToast>();
            toast.Show("正在加载...");

            this.IsBusy = true;

            var page = isReload ? 0 : this.NextPage;
            var result = await this.GetDatas(page);

            if (!result.Item1 && result.Item2 != null && result.Item2.Count() > 0) {
                if (isReload)
                    this.Datas.Clear();

                this.NextPage = page + 1;
                this.Datas.AddRange(result.Item2);
            } else if (result.Item1) {
                toast.Show("加载失败");
            } else if (result.Item2?.Count() == 0) {
                toast.Show("没有更多");
            }
            this.IsBusy = false;
        }
    }
}
