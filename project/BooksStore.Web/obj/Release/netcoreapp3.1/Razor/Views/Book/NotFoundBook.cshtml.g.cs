#pragma checksum "C:\IT\c#\Проекты\BooksStore\project\BooksStore.Web\Views\Book\NotFoundBook.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2d83a1a8b0075f68a06a3ff5c30182ed23676b56"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Book_NotFoundBook), @"mvc.1.0.view", @"/Views/Book/NotFoundBook.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\IT\c#\Проекты\BooksStore\project\BooksStore.Web\Views\_ViewImports.cshtml"
using BooksStore.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\IT\c#\Проекты\BooksStore\project\BooksStore.Web\Views\_ViewImports.cshtml"
using BooksStore.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\IT\c#\Проекты\BooksStore\project\BooksStore.Web\Views\_ViewImports.cshtml"
using BooksStore.Web.Models.ViewModel.Index;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\IT\c#\Проекты\BooksStore\project\BooksStore.Web\Views\_ViewImports.cshtml"
using BooksStore.Web.Models.ViewModel.UpdateModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\IT\c#\Проекты\BooksStore\project\BooksStore.Web\Views\_ViewImports.cshtml"
using BooksStore.Web.Models.ViewModel.CreateModel;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\IT\c#\Проекты\BooksStore\project\BooksStore.Web\Views\_ViewImports.cshtml"
using BooksStore.Web.Models.ViewModel.ReadModel;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2d83a1a8b0075f68a06a3ff5c30182ed23676b56", @"/Views/Book/NotFoundBook.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23a95437b57fa1f82e40a1ec6de41642d36f86d0", @"/Views/_ViewImports.cshtml")]
    public class Views_Book_NotFoundBook : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<NotFountBookModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\IT\c#\Проекты\BooksStore\project\BooksStore.Web\Views\Book\NotFoundBook.cshtml"
Write(await Html.PartialAsync("SearchPartial"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"notFountBookByName\">\r\n    <h3>");
#nullable restore
#line 5 "C:\IT\c#\Проекты\BooksStore\project\BooksStore.Web\Views\Book\NotFoundBook.cshtml"
   Write(Model.ErrorMassage);

#line default
#line hidden
#nullable disable
            WriteLiteral(": ");
#nullable restore
#line 5 "C:\IT\c#\Проекты\BooksStore\project\BooksStore.Web\Views\Book\NotFoundBook.cshtml"
                        Write(Model.Value);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<NotFountBookModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
