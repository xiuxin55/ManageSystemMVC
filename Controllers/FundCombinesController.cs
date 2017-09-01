using DGBank.AppManager.Comm;
using DGBank.BLL;
using DGBank.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebManager.Comm;

namespace WebManager.Controllers
{
    public class FundCombinesController : Controller
    {
        //
        //获取组合列表
        #region 获取组合列表
        [HttpPost]
        public ActionResult FundCombinesList()
        {
            initFundCombines();
            Comm.Common.SaveLog(url, "查看基金组合列表界面");
            return View(dataSourceList);
        }
        #region 属性
        /// <summary>
        /// source
        /// </summary>
        List<TblFundCombines> dataSourceList = new List<TblFundCombines>();

        FundInfoBLL fundBll = new FundInfoBLL();//基金bll
        TblFundCombinesRecordsBLL combinesRecords = new TblFundCombinesRecordsBLL();
        string url = "FundCombines/FundCombinesList";

        /// <summary>
        /// 基金组合bll
        /// </summary>
        TblFundCombinesBLL bll = new TblFundCombinesBLL();
        #endregion
        /// <summary>
        /// FundCombines列表
        /// </summary>
        private void initFundCombines()
        {
            if (dataSourceList.Count > 0)
                dataSourceList = new List<TblFundCombines>();

            var source = bll.FindFundCombinesDisplayOrder();

            if (source.Count == 0)
            {
                return;
            }
            dataSourceList = source.OrderBy(e => e.DisplayOrder).ToList();
            Session["Combine_TopOrder"] = dataSourceList.FirstOrDefault()!=null? new Nullable<int>(dataSourceList.FirstOrDefault().DisplayOrder):null;
        }

       
        /// <summary>
        /// grid_RowDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void grid_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (e.Row.RowIndex == 0)
        //        {
        //            ((WebControl)e.Row.FindControl("moveUp")).Enabled = false;
        //            ((WebControl)e.Row.FindControl("top")).Enabled = false;
        //        }
        //        else if (e.Row.RowIndex == dataSourceList.Count - 1)
        //        {
        //            ((WebControl)e.Row.FindControl("moveDown")).Enabled = false;
        //        }
        //    }
        //}

         //<summary>
         //操作【编辑、删除、上/下移、置顶】
         //</summary>
         //<param name="sender"></param>
         //<param name="e"></param>
        [HttpPost]
        public JsonResult LinkOperate(int Id, string paramtype, int DisplayOrder, string AllIds)
        {
           
            switch (paramtype)
            {
                case "del":
                    #region 删除逻辑
                    TblFundCombines curItem = bll.GetById(Id);
                    string delcontent = string.Format("基金组合列表:删除{0}", curItem.Name);
                    Comm.Common.SaveLog(url, delcontent);

                    bll.Delete(curItem);
                    combinesRecords.DeleteByCombinesId(curItem.Id.ToString());
                    updateFundCombinesRedis(curItem, true);
                    #endregion
                    break;
                case "moveup":
                    #region move up
                       
                        int index=AllIds.Split(',').ToList().IndexOf(Id.ToString());
                        int upId = int.Parse(AllIds.Split(',')[index - 1]);
                        var content = bll.GetById(Id);
                       
                        content.LastUpdateDate = DateTime.Now;
                        var upcontent = bll.GetById(upId);
                        content.DisplayOrder = upcontent.DisplayOrder;
                        upcontent.DisplayOrder = DisplayOrder;
                        upcontent.LastUpdateDate = DateTime.Now;
                       
                        if (bll.Update(content) > 0 && bll.Update(upcontent) > 0)
                        {
                            List<TblFundCombines> updateList = new List<TblFundCombines>();
                            updateList.Add(content);
                            updateList.Add(upcontent);
                            updateFundCombinesRedis(updateList);
                            string moveupcontent = string.Format("基金组合列表:上移{0}", content.Name);
                            Comm.Common.SaveLog(url, moveupcontent);
                           
                        }
                        else
                        {
                            return Json(new { state = "error", message = "当前数据发生变化，请刷新网页" });
                        }
                        break;
                    #endregion
                case "movedown":
                    #region move down

                        index=AllIds.Split(',').ToList().IndexOf(Id.ToString());
                        int downId = int.Parse(AllIds.Split(',')[index + 1]);
                        content = bll.GetById(Id);
                        content.LastUpdateDate = DateTime.Now;
                        var downcontent = bll.GetById(downId);
                        content.DisplayOrder = downcontent.DisplayOrder;
                        downcontent.DisplayOrder = DisplayOrder;
                        downcontent.LastUpdateDate = DateTime.Now;
                        if (bll.Update(content) > 0 && bll.Update(downcontent) > 0)
                        {
                            List<TblFundCombines> updateList = new List<TblFundCombines>();
                            updateList.Add(content);
                            updateList.Add(downcontent);
                            updateFundCombinesRedis(updateList);
                            string movedowncontent = string.Format("基金组合列表:下移{0}", content.Name);
                            Comm.Common.SaveLog(url, movedowncontent);
                           
                        }
                        else
                        {
                            return Json(new { state = "error", message = "当前数据发生变化，请刷新网页" });
                        }
                    #endregion
                        break;
                case "top":
                    if (Session["Combine_TopOrder"]==null)
                    {
                        return Json(new { state = "error", message = "连接已断开，请刷新网页" });
                    }
                    #region top
                    int topDisOrder = int.Parse(Session["Combine_TopOrder"].ToString());
                    var topcontent = bll.GetById(Id);
                    topcontent.DisplayOrder = topDisOrder - 1;
                    topcontent.LastUpdateDate = DateTime.Now;
                    if (bll.Update(topcontent) > 0)
                    {
                        updateFundCombinesRedis(topcontent);
                        string toplogcontent = string.Format("基金组合列表:置顶{0}", topcontent.Name);
                        Comm.Common.SaveLog(url, toplogcontent);
                    }
                    else
                    {
                        return Json(new { state = "error", message = "当前数据发生变化，请刷新网页" });
                    }
                    #endregion
                    break;
                default:
                    return Json(new { state = "error", message = "操作类型不存在" });
                //case "history":
                //    hfCombinesID.Value = id;
                //    ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>RecordsList();</script>");   //获取调仓历史记录
                //    break;
            }
            return Json(new { state = "success", message = "成功" });
           
        }

        /// <summary>
        /// 更新组合基金redis
        /// </summary>
        void updateFundCombinesRedis(TblFundCombines enitity, bool isDel = false)
        {
            RedisOperate redis = new RedisOperate();
            List<TblFundCombines> combines = new List<TblFundCombines>();
            combines.Add(enitity);
            redis.SetFundCombinesRedis(combines, isDel);
        }

        /// <summary>
        /// 更新组合基金redis
        /// </summary>
        void updateFundCombinesRedis(List<TblFundCombines> list)
        {
            RedisOperate redis = new RedisOperate();
            redis.SetFundCombinesRedis(list);
        }
        #endregion
        

    }
}
