<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="LoginApplication.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <br />
    <br />
    <br />
    <br />
    <div class="container">
        <div class="row">
            <div class="col-md-6 mx-auto">
                <div class="card text-white bg-dark mb-3" style="max-width: 85rem;">
                    <div class="card-body">

                        <div class="row">
                            <div class="col">
                                <div style="text-align: center">
                                    <img width="150px" src="IMAGES/generaluser.png" />
                                    <div style="text-align: center">
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div style="text-align: center">
                                    <h3>User Login</h3>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <div style="text-align: center">
                                    <hr>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col">
                                <label>Member ID</label>
                                <div class="form-group">
                                    <asp:TextBox CssClass="form-control" ID="TextBox1" runat="server" placeholder="Member ID"></asp:TextBox>

                                    <label>Password</label>

                                    <div class="form-group">
                                        <asp:TextBox CssClass="form-control" ID="TextBox2" runat="server" placeholder="Password" TextMode="Password"></asp:TextBox>
                                    </div>

                                    <div class="form-group">
                                        <asp:Button class="btn btn-success btn-block btn-lg" ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
                <br>
            </div>
        </div>
    </div>


</asp:Content>
