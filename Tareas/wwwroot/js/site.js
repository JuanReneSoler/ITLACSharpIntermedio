// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function Binding(tag, propertyBinding, tableToPresent, value)
{
    if(!$(tag).prop("checked"))
    {
        let input = $("#"+tableToPresent).find("input[value='"+tag.id+"']").parent();
        return Remove(input, tableToPresent, propertyBinding);
    }
    Append(tableToPresent, propertyBinding, tag.id, value);

}

function addAtaque(search, tagBinding, propertyBinding)
{
    let text = $("#"+search).val();
    Append(tagBinding, propertyBinding, text, text);
}

function Append(id, property, value, text)
{
    let count = $("#"+id).find("li").length;
    $("#"+id).append(`
        <li>
            <input name='`+property+`[`+count+`]' type='hidden' value='`+value+`' />
            `+text+` 
            <input type='button' class='btn btn-danger' value='-' onclick="Remove($(this).parent(), '`+id+`', '`+property+`')">
        </li>`);
}

function Remove(tag, idTable, property)
{
    var val = $(tag).parent().find("input[type='hidden']").val();
    $(tag).remove();
    let count = $("#"+idTable).find("li").length;
    let inputs = $("#"+idTable).find("input");
    for(let c=0; c<=count;c++)
    {
        $(inputs[c]).prop("name", property+"["+c+"]");
        $("input[type$='checkbox'][id$='"+val+"']").prop("checked", false);
    }
}