using CYTX.Utility;
using CYTX.Utility.Constants;
using CYTX.Utility.DataHelper;
/*
Author:格式：WangXinXin 
Create date: 2017/4/19 
Description：后台Manager实时更新Redis
--------------------------------------------------------------------------------------------------------
Versions：
    V1.00 2017/4/19 WangXinXin Add
*/
using DGBank.Cache;
using DGBank.Common;
using DGBank.Entities;
using DGBank.Models;
using DGBank.Models.App;
using DGBank.Services;
using DGBank.WSCache;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WebManager.Comm
{
    public class RedisOperate : RemoteCacheBase
    {

        #region Manager广告
        /// <summary>
        /// 更新广告Redis
        /// </summary>
        /// <param name="obj">存储到Redis对象</param>
        /// <returns></returns>
        public void SetADSRedis()
        {
            NewsCache fundCache = new NewsCache();

            fundCache.RefreshAdList();
        }
        #endregion

        #region 基金推荐
        /// <summary>
        /// 更新基金推荐Redis
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public void SetRecommandFundRedis(IList<TblFundRecommends> funds, bool isDel = false)
        {
            FundsCache fundCache = new FundsCache();
            List<DGBank.Cache.CFundService.TblFundRecommends> list = new List<Cache.CFundService.TblFundRecommends>();
            //转换对象类型
            if (null != funds)
            {
                foreach (var item in funds)
                {
                    DGBank.Cache.CFundService.TblFundRecommends curItem = EntityConv(item);
                    list.Add(curItem);
                }
            }
            fundCache.SetRecommendFundsRedis(list, isDel);
        }


        /// <summary>
        /// 实体转换
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static Cache.CFundService.TblFundRecommends EntityConv(TblFundRecommends item)
        {
            DGBank.Cache.CFundService.TblFundRecommends curItem = new Cache.CFundService.TblFundRecommends();
            curItem.Id = item.Id;
            curItem.m_create_date = item.CreateDate;
            curItem.m_display_field = item.DisplayField;
            curItem.m_display_order = item.DisplayOrder;
            curItem.m_fund_code = item.FundCode;
            curItem.m_kind = item.Kind;
            curItem.subKindType = item.SubKindType;
            curItem.m_last_update_date = item.LastUpdateDate;
            curItem.m_remark = item.Remark;
            return curItem;
        }

        #endregion

        #region 基金组合
        /// <summary>
        /// 更新基金组合Redis
        /// </summary>
        ///<param name="fundCombines">基金组合列表</param>
        ///<param name="isDel">ture->删除</param>
        /// <returns></returns>
        public void SetFundCombinesRedis(List<TblFundCombines> fundCombinesList, bool isDel = false)
        {
            FundsCache fundCache = new FundsCache();
            List<DGBank.Cache.CFundService.TblFundCombines> list = new List<Cache.CFundService.TblFundCombines>();
            //转换对象类型
            if (null != fundCombinesList)
            {
                foreach (var item in fundCombinesList)
                {
                    DGBank.Cache.CFundService.TblFundCombines curItem = EntityConv(item);
                    list.Add(curItem);
                }
            }
            fundCache.SetFundCombinesRedis(list, isDel);
        }


        /// <summary>
        /// 实体转换
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private static Cache.CFundService.TblFundCombines EntityConv(TblFundCombines item)
        {
            DGBank.Cache.CFundService.TblFundCombines curItem = new Cache.CFundService.TblFundCombines();
            curItem.Id = item.Id;
            curItem.m_create_date = item.CreateDate;
            curItem.m_display_field = item.DisplayField;
            curItem.m_display_order = item.DisplayOrder;
            curItem.m_funds = item.Funds;
            curItem.m_invest_type = item.InvestType;
            curItem.m_last_update_date = item.LastUpdateDate;
            curItem.m_min_deposit = item.MinDeposit;
            curItem.m_name = item.Name;
            curItem.m_org_code = item.OrgCode;
            curItem.combineType = item.CombineType;
            return curItem;
        }

        #endregion
    }
}