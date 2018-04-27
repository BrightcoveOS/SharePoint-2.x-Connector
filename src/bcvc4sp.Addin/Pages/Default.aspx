﻿<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~site/_catalogs/masterpage/BrightcoveApp.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
    <SharePoint:ScriptLink name="sp.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <meta name="WebPartPageExpansion" content="full" />
    <!-- Reference to the production css bundle. Update the hash after a build. -->
    <link rel="Stylesheet" type="text/css" href="../static/css/vendor.da20cd26.css" />
    <link rel="Stylesheet" type="text/css" href="../static/css/main.a1d9c6d4.css" />
</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Brightcove - Video Connector for SharePoint
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">
    <noscript>
        You need to enable JavaScript to run this app.
    </noscript>
    <div id="root"></div>
    <!-- Reference to the production bundle. Update the hashes after a build. -->
    <script type="text/javascript" src="../static/js/runtime.e03e84a5.js"></script>
    <script type="text/javascript" src="../static/js/vendor.7eece66a.js"></script>
    <script type="text/javascript" src="../static/js/main.86e63af8.js"></script>

    <!-- Reference to the dev bundle. Uncomment during development. -->
<%--    <script type="text/javascript" src="http://localhost:3000/static/js/runtime.js"></script>
    <script type="text/javascript" src="http://localhost:3000/static/js/vendor.js"></script>
    <script type="text/javascript" src="http://localhost:3000/static/js/main.js"></script>--%>
</asp:Content>