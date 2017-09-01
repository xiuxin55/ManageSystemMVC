         $(function(){
            //基金产品tab 切换
            $(".tab ul li").on("click", function(){
                       $(this).addClass("select").siblings().removeClass("select");
                       var index = $(this).index();
                       $(".tab_con > div").eq(index).show().siblings().hide();
            });
            //基金产品二级tab切换
            $(".second_tab a").on("mouseover",function(){
                      $(this).addClass("now").siblings().removeClass("now");
                      var index = $(this).index();
                  
                      $(".second_con > div").eq(index).show().siblings().hide();
            });
            //基金产品table鼠标滑过
            $(".con_table >tbody>tr").on("mouseover",function(){  
                $(this).css("background","#f3f8fd");  
            });  
            $(".con_table>tbody>tr").on("mouseout",function(){  
                $(this).css("background","#ffffff");  
            });   
            $('.order').on("click",function(){
                var spanChild=$(this).children('span');
                if(spanChild.hasClass('uporder')){
                    spanChild.removeClass('uporder').addClass('downorder');
                }
                else if(spanChild.hasClass('downorder')){
                    spanChild.removeClass('downorder').addClass('uporder');
                }
                else{
                   spanChild.addClass('downorder');
                }
            }) 
             //我的关注表格选中与否checkbox
             $(".tabel_checkbox").click(function(){
                       var that=$(this);
                           if(that.hasClass('nocheck')){
                                that.removeClass('nocheck');
                                that.addClass('check');
                           }
                           else{
                                that.addClass('nocheck');
                                that.removeClass('check');

                           }           
             });
             //基金组合页面收益走势交互
             $(".income_time li").on("click", function(){
                           $(this).addClass("nowtime").siblings().removeClass("nowtime");
                           var index = $(this).index();
                           $("#container").html('内容'+index);
             });
             //基金组合页面鼠标滑过效果去掉
              $(".group_table >tbody>tr").on("mouseover",function(){  
                $(this).css("background","#fff");  
             });
             //基金诊断-基金能力诊断切换
              $(".ability_target ul li").on("click",function(){
                        $(this).addClass("nowtime01").siblings().removeClass("nowtime01");
                        var index = $(this).index();
                        $(".target_text > div").eq(index).show().siblings().hide();
              });  
           
       });