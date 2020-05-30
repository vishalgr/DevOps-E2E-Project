<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeFile="Homepage.aspx.cs" Inherits="LoginApplication.Homepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

     <section>
        <img src="IMAGES/2.jpg" class="img-fluid" />
    </section>

    <section>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div style="text-align: center">
                        <h2>Our Features</h2>
                        <p><b>Our 2 Primary Features</b></p>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <div class="card text-white bg-dark mb-3" style="max-width: 90rem;">
                        <div class="card-body">
                            <div style="text-align: center">
                                <img width="150px" src="IMAGES/3.png" />
                                <h4>Continuous Integration (CI)</h4>
                                <p class="text-justify">
                                    Continuous integration (CI) is the process of automatically detecting, pulling, building, 
                                    and (in most cases) doing unit testing as source code is changed for a product. CI is the activity that starts
                                    the pipeline (although certain pre-validations—often called "pre-flight checks"—are sometimes incorporated ahead of CI).
                                    The goal of CI is to quickly make sure a new change from a developer is "good" and suitable for further use in the code base.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="col-md-6">
                    <div class="card text-white bg-dark mb-3" style="max-width: 90rem;">
                        <div class="card-body">
                            <div style="text-align: center">
                                <img width="150px" src="IMAGES/4.png" />
                                <h4>Continuous Deployment (CD)</h4>
                                <p class="text-justify">
                                    Continuous delivery (CD) Continuous Deployment (CD) is a software release process that uses automated testing to validate if 
                            changes to a codebase are correct and stable for immediate autonomous deployment to a production environment.The software release
                            cycle has evolved over time. The legacy process of moving code from one machine to another and checking if it works as expected
                            used to be  an error prone and resource-heavy process. 
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <section>
        <img src="IMAGES/5.jpg" class="img-fluid" />
    </section>

    <section>
        <div class="container">
            <div class="row">
                <div class="col-12">
                    <div style="text-align: center">
                        <h2>Our Process</h2>
                        <p><b>We have a 3 Step Process</b></p>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-4">
                    <div class="card text-white bg-info mb-3" style="max-width: 90rem;">
                        <div class="card-body ">
                            <div style="text-align: center">
                                <img width="150px" src="IMAGES/8.png" />
                                <h4>Creating a Pipeline</h4>
                                <p class="text-justify">
                                    Jenkins Pipeline (or simply "Pipeline") is a suite of plugins which supports
                                    implementing and integrating continuous delivery pipelines into Jenkins.A continuous delivery
                                    pipeline is an automated expression of your process for getting software from version control
                                    right through to your users and customers.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="card text-white bg-info mb-3" style="max-width: 90rem;">
                        <div class="card-body ">
                            <div style="text-align: center">
                                <img width="150px" src="IMAGES/7.png" />
                                <h4>Build Step</h4>
                                <p class="text-justify">
                                    Triggers a new build for a given job.Name of a downstream job to build.
                                    May be another Pipeline job, but more commonly a freestyle or other project. Use a simple name
                                    if the job is in the same folder as this upstream Pipeline job; otherwise can use relativepaths
                                    like ../sister-folder/downstream or absolute paths like /top-level-folder/nested-folder/downstream. 
                                </p>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-4">
                    <div class="card text-white bg-info mb-3" style="max-width: 90rem;">
                        <div class="card-body ">
                            <div style="text-align: center">
                                <img width="150px" src="IMAGES/6.png" />
                                <h4>Test Step</h4>
                                <p class="text-justify">
                                    Testing in DevOps spans the whole software development and delivery lifecycle.
                                    Testers are no longer just focusing on functional testing and feature verification.As testers, we
                                    should also be involved in operations testing, performance testing, basic security testing, as
                                    well as being able to monitor and analyze production data and logs. 
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
