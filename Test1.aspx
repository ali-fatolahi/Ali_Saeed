<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Test1.aspx.cs" Inherits="BingMapTest_Test1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=7.0"></script>
<script type="text/javascript">
    var map=null;
    function GetMap() {
        map = new Microsoft.Maps.Map(document.getElementById("mapDiv"),
            { credentials: "ArLeGdHOcc5h7j3L4W37oFGcU9E-LF3tAZi4o0DfhXbPJ8aiyTGbIDNHex08R2u7",
                center: new Microsoft.Maps.Location(54, -2),
                       mapTypeId: Microsoft.Maps.MapTypeId.road,
                       zoom: 10,
                       showDashboard: false
            });
    }

    function SetZoom() {
        var zmLevel = parseInt(document.getElementById('<%=TextBox1.ClientID%>').value);
        alert(zmLevel);
        map.setView({ zoom:zmLevel });
    }

    function invokeMeMaster() {
        alert('I was invoked from Master');
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <body onload="GetMap();">
        <div id='mapDiv' style="position:relative; width:400px; height:400px;"></div>
    </body>
    <asp:TextBox ID="TextBox1" runat="server" Text="17"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"></asp:Button>
    <asp:Button ID="Button2" runat="server" Text="Button" OnClientClick="SetSize();"></asp:Button>
    <asp:TextBox ID="txtZoom" runat="server"></asp:TextBox>
</asp:Content>

