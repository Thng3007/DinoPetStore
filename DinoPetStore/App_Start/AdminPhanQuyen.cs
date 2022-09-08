﻿using DinoPetStore.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DinoPetStore.Models;

namespace DinoPetStore.App_Start
{
    public class AdminPhanQuyen : AuthorizeAttribute
    {
        DinoStoreDbContext data = new DinoStoreDbContext();
        public int MACN { set; get; }
        public string MACHUCNANG { set; get; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {

            ADMIN quantri = (ADMIN)HttpContext.Current.Session["Taikhoanadmin"];
            if (quantri != null)
            {
                #region Check quyền  quản trị viên

                var count = data.PHANQUYENs.Count(m => m.MAADMIN == quantri.MAADMIN & m.MACHUCNANG == MACHUCNANG);
                if (count != 0)
                {
                    return;
                }
                else
                {
                    var returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                    filterContext.Result = new RedirectToRouteResult(new
                        RouteValueDictionary
                        (
                            new
                            {
                                controller = "BaoLoi",
                                action = "KhongCoQuyen",
                                area = "Admin",
                                returnUrl = returnUrl.ToString()
                            }
                        ));
                }
                #endregion
                return;
            }
            else
            {
                var returnUrl = filterContext.RequestContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary
                    (
                        new
                        {
                            controller = "Admin",
                            action = "Index",
                            area = "Admin",
                            returnUrl = returnUrl.ToString()
                        }
                    ));
            }
        }

    }
}