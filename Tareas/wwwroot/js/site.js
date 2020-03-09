// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function Binding(tag, propertyBinding, tableToPresent, value)
{
    if(!$(tag).prop("checked"))
    {
        $(tag).attr("id", "");
        $(tag).attr("name", "");
        $("#"+tableToPresent).find("li[id='"+value+"']").remove();
        return;
    }
    var guid = $(tag).parents("tbody").find("input:checked").length-1;
    $(tag).attr("name", propertyBinding+"["+guid+"]");
    $("#"+tableToPresent).append("<li id='"+value+"'>"+value+"</li>");
}

function addAtaque(search, tagBinding, propertyBinding)
{
    var text = $("#"+search).val();
    var guid = $("#"+tagBinding).find("li").length;
    $("#"+tagBinding).append("<li name='"+propertyBinding+"["+guid+"]' value='"+text+"'>"+text+"</li>");
}
