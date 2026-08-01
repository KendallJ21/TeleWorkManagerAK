<%@ Page Language="C#" AutoEventWireup="true"
CodeBehind="DashboardEmployeePage.aspx.cs"
Inherits="TeleWorkManager.Pages.DashboardEmployeePage" %>

<!doctype html>

<html>
   <head runat="server">
      <title>Dashboard - TeleWork Manager</title>

      <link
         href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css"
         rel="stylesheet"
      />

      <link
         href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.css"
         rel="stylesheet"
      />

      <link href="../CSS/DashboardEmployeePage.css" rel="stylesheet" />
   </head>

   <body>
      <form id="form1" runat="server">
         <div class="container py-4">
            <!-- TITULO -->

            <div class="text-center mb-4">
               <h1>
                  <i class="bi bi-house-door-fill"></i>

                  TeleWork Manager
               </h1>

               <h4>Dashboard - Kendall</h4>
            </div>

            <!-- RESUMEN -->

            <div class="row g-4">
               <div class="col-md-3">
                  <div class="card-dashboard text-center">
                     <i class="bi bi-house-door icon-dashboard"></i>

                     <h5 class="letra mt-3">Días asignados</h5>

                     <h1 class="title">8</h1>

                     <p class="letra">Julio 2026</p>
                  </div>
               </div>

               <div class="col-md-3">
                  <div class="card-dashboard text-center">
                     <i class="bi bi-calendar-event icon-dashboard"></i>

                     <h5 class="letra mt-3">Próximos días</h5>

                     <h1 class="title">3</h1>

                     <p class="letra">Agosto</p>
                  </div>
               </div>

               <div class="col-md-3">
                  <div class="card-dashboard text-center">
                     <i class="bi bi-file-earmark-text icon-dashboard"></i>

                     <h5 class="letra mt-3">Solicitudes</h5>

                     <h1 class="title">2</h1>

                     <span class="badge bg-warning">Pendientes </span>
                  </div>
               </div>

               <div class="col-md-3">
                  <div class="card-dashboard text-center">
                     <i class="bi bi-bell icon-dashboard"></i>

                     <h5 class="letra mt-3">Notificaciones</h5>

                     <h1 class="title">4</h1>

                     <span class="badge bg-success">Nuevas </span>
                  </div>
               </div>
            </div>

            <br />

            <!-- RESUMEN -->

            <div class="login-card p-4">
               <h4 class="title">Resumen mensual</h4>

               <p class="letra">Días utilizados: 8 / 12</p>

               <div class="progress">
                  <div class="progress-bar" style="width: 67%">67%</div>
               </div>
            </div>
            <br />
            <div class="login-card p-4">
               <h4 class="title mb-4">
                  <i class="bi bi-calendar-check"></i>
                  Solicitud de Teletrabajo
               </h4>

               <div class="row">
                  <!-- Calendario -->
                  <div class="col-lg-7">
                     <asp:Calendar
                        ID="Calendar1"
                        runat="server"
                        CssClass="calendar-full"
                        BackColor="White"
                        BorderColor="Black"
                        BorderStyle="Solid"
                        CellSpacing="1"
                        Font-Names="Verdana"
                        Font-Size="9pt"
                        ForeColor="Black"
                        Width="100%"
                        Height="350px"
                        NextPrevFormat="ShortMonth"
                     >
                        <DayHeaderStyle
                           Font-Bold="True"
                           Font-Size="8pt"
                           ForeColor="#333333"
                        />

                        <DayStyle BackColor="#CCCCCC" />

                        <NextPrevStyle
                           Font-Bold="True"
                           Font-Size="8pt"
                           ForeColor="White"
                        />

                        <OtherMonthDayStyle ForeColor="#999999" />

                        <SelectedDayStyle
                           BackColor="#00ADB5"
                           ForeColor="White"
                        />

                        <TitleStyle
                           BackColor="#00ADB5"
                           Font-Bold="True"
                           Font-Size="12pt"
                           ForeColor="White"
                        />

                        <TodayDayStyle BackColor="#999999" ForeColor="White" />
                     </asp:Calendar>
                  </div>

                  <!-- Formulario -->
                  <div class="col-lg-5">
                     <label class="form-label letra mt-2">
                        Fecha seleccionada
                     </label>

                     <asp:TextBox
                        ID="txtFecha"
                        runat="server"
                        CssClass="form-control mb-3"
                        ReadOnly="true"
                     >
                     </asp:TextBox>

                     <label class="form-label letra">
                        Motivo de la solicitud
                     </label>

                     <asp:TextBox
                        ID="txtMotivo"
                        runat="server"
                        CssClass="form-control mb-3"
                        TextMode="MultiLine"
                        Rows="5"
                        placeholder="Ingrese el motivo de la solicitud..."
                     >
                     </asp:TextBox>

                     <label class="form-label letra"> Observaciones </label>

                     <asp:TextBox
                        ID="txtObservacion"
                        runat="server"
                        CssClass="form-control mb-4"
                        TextMode="MultiLine"
                        Rows="3"
                        placeholder="Observaciones adicionales (opcional)"
                     >
                     </asp:TextBox>

                     <div class="d-grid gap-2">
                        <asp:Button
                           ID="btnSolicitar"
                           runat="server"
                           Text="Enviar Solicitud"
                           CssClass="btn btn-custom btn-lg"
                        />

                        <asp:Button
                           ID="btnLimpiar"
                           runat="server"
                           Text="Limpiar"
                           CssClass="btn btn-outline-light"
                        />
                     </div>
                  </div>
               </div>
            </div>
            <br />

            <br />

            <br />
         </div>
      </form>
   </body>
</html>
