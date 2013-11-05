<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Quote.aspx.cs" Inherits="HealthCareDotGov.Web.Quote" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <p>
            SSN: <asp:TextBox ID="txtSSN" runat="server"></asp:TextBox>
        </p>   
        <p>
            Birth Year: 
            <asp:DropDownList runat="server" ID="lstBirthYear"/>
        </p>
      <p>
          Policy Type:
        <asp:DropDownList ID="lstPolicyType" runat="server">
            <Items>
                <asp:ListItem Text="Single"></asp:ListItem>
                <asp:ListItem Text="Married"></asp:ListItem>
                <asp:ListItem Text="Family"></asp:ListItem>
                <asp:ListItem Text="Any"></asp:ListItem>
            </Items>
        </asp:DropDownList>
      </p>
        <p>
            <asp:Button runat="server" Text="Submit" OnClick="GetQuote"/>
        </p>
    </div>
      <div>
          <h3 id="ResultsMsg" runat="server"></h3>
          <asp:DataGrid ID="resultsGrid" AutoGenerateColumns="true" runat="server"></asp:DataGrid>
      </div>
    </form>
</body>
</html>
