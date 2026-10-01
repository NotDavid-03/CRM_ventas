using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using CRM_Nexus_Retail.DataAccess;

namespace CRM_Nexus_Retail.Controllers
{
    public class ReportesController : Controller
    {
        private readonly ReportesDAL _dalReportes = new ReportesDAL();

        // REPORTE 1: Ventas por Sucursal, Cajero y Segmento
        public ActionResult Reporte1(DateTime? fechaInicio, DateTime? fechaFin, int? idSucursal, int? idSegmento, int? idColaborador)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@Fecha_Inicio", (object)fechaInicio ?? DBNull.Value),
                new SqlParameter("@Fecha_Fin", (object)fechaFin ?? DBNull.Value),
                new SqlParameter("@Id_Sucursal", (object)idSucursal ?? DBNull.Value),
                new SqlParameter("@Id_Segmento", (object)idSegmento ?? DBNull.Value),
                new SqlParameter("@Id_Colaborador", (object)idColaborador ?? DBNull.Value)
            };

            DataTable datos = _dalReportes.EjecutarReporte("SP_Rep1_Ventas_Sucursal_Cajero_Segmento", parametros);
            ViewBag.NombreReporte = "1. Ventas por Sucursal, Cajero y Segmento CRM";
            return View("VisorReporte", datos);
        }

        // REPORTE 2: Rentabilidad de Productos y Promociones
        public ActionResult Reporte2(int? idCategoria, int? idMarca, int? idPromocion, decimal? montoMinimo, string estadoFactura)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_Categoria", (object)idCategoria ?? DBNull.Value),
                new SqlParameter("@Id_Marca", (object)idMarca ?? DBNull.Value),
                new SqlParameter("@Id_Promocion", (object)idPromocion ?? DBNull.Value),
                new SqlParameter("@Monto_Minimo", (object)montoMinimo ?? DBNull.Value),
                new SqlParameter("@Estado_Factura", (object)estadoFactura ?? DBNull.Value)
            };

            DataTable datos = _dalReportes.EjecutarReporte("SP_Rep2_Rentabilidad_Productos_Promocion", parametros);
            ViewBag.NombreReporte = "2. Rentabilidad de Productos y Promociones";
            return View("VisorReporte", datos);
        }

        // REPORTE 3: Rendimiento de Campañas Marketing
        public ActionResult Reporte3(int? idCampana, int? idCanal, int? minSaldoPuntos, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_Campana", (object)idCampana ?? DBNull.Value),
                new SqlParameter("@Id_Canal", (object)idCanal ?? DBNull.Value),
                new SqlParameter("@Min_Saldo_Puntos", (object)minSaldoPuntos ?? DBNull.Value),
                new SqlParameter("@Fecha_Desde", (object)fechaDesde ?? DBNull.Value),
                new SqlParameter("@Fecha_Hasta", (object)fechaHasta ?? DBNull.Value)
            };

            DataTable datos = _dalReportes.EjecutarReporte("SP_Rep3_Rendimiento_Campanas_Conversion", parametros);
            ViewBag.NombreReporte = "3. Rendimiento de Campañas de Mercadeo y Conversión";
            return View("VisorReporte", datos);
        }

        // REPORTE 4: Arqueo de Pagos por Terminal y Turno
        public ActionResult Reporte4(int? idSucursal, int? idMetodoPago, int? idTerminal, decimal? montoPagoMinimo, int? idCajero)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_Sucursal", (object)idSucursal ?? DBNull.Value),
                new SqlParameter("@Id_Metodo_Pago", (object)idMetodoPago ?? DBNull.Value),
                new SqlParameter("@Id_Terminal", (object)idTerminal ?? DBNull.Value),
                new SqlParameter("@Monto_Pago_Minimo", (object)montoPagoMinimo ?? DBNull.Value),
                new SqlParameter("@Id_Cajero", (object)idCajero ?? DBNull.Value)
            };

            DataTable datos = _dalReportes.EjecutarReporte("SP_Rep4_Arqueo_Pagos_Terminal_Metodo", parametros);
            ViewBag.NombreReporte = "4. Arqueo de Pagos por Terminal y Método de Cobro";
            return View("VisorReporte", datos);
        }

        // REPORTE 5: Canjes de Puntos por Afinidad
        public ActionResult Reporte5(int? idSucursal, int? idRecompensa, int? idInteres, int? puntosCanjeadosMin, DateTime? fechaCanjeInicio)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_Sucursal", (object)idSucursal ?? DBNull.Value),
                new SqlParameter("@Id_Recompensa", (object)idRecompensa ?? DBNull.Value),
                new SqlParameter("@Id_Interes", (object)idInteres ?? DBNull.Value),
                new SqlParameter("@Puntos_Canjeados_Min", (object)puntosCanjeadosMin ?? DBNull.Value),
                new SqlParameter("@Fecha_Canje_Inicio", (object)fechaCanjeInicio ?? DBNull.Value)
            };

            DataTable datos = _dalReportes.EjecutarReporte("SP_Rep5_Canjes_Puntos_Afinidad_Recompensa", parametros);
            ViewBag.NombreReporte = "5. Canjes de Puntos por Afinidad y Recompensas";
            return View("VisorReporte", datos);
        }

        // REPORTE 6: Trazabilidad de Kardex y Proveedor
        public ActionResult Reporte6(int? idBodega, int? idProveedor, int? idProducto, string tipoMovimiento, DateTime? fechaMovimientoInicio)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_Bodega", (object)idBodega ?? DBNull.Value),
                new SqlParameter("@Id_Proveedor", (object)idProveedor ?? DBNull.Value),
                new SqlParameter("@Id_Producto", (object)idProducto ?? DBNull.Value),
                new SqlParameter("@Tipo_Movimiento", (object)tipoMovimiento ?? DBNull.Value),
                new SqlParameter("@Fecha_Movimiento_Inicio", (object)fechaMovimientoInicio ?? DBNull.Value)
            };

            DataTable datos = _dalReportes.EjecutarReporte("SP_Rep6_Trazabilidad_Kardex_Proveedor", parametros);
            ViewBag.NombreReporte = "6. Trazabilidad de Kardex y Proveedores";
            return View("VisorReporte", datos);
        }

        // REPORTE 7: Productividad de Colaboradores
        public ActionResult Reporte7(int? idRol, int? idSucursal, int? idPuesto, bool? estadoColaborador, DateTime? fechaVentaInicio)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_Rol", (object)idRol ?? DBNull.Value),
                new SqlParameter("@Id_Sucursal", (object)idSucursal ?? DBNull.Value),
                new SqlParameter("@Id_Puesto", (object)idPuesto ?? DBNull.Value),
                new SqlParameter("@Estado_Colaborador", (object)estadoColaborador ?? DBNull.Value),
                new SqlParameter("@Fecha_Venta_Inicio", (object)fechaVentaInicio ?? DBNull.Value)
            };

            DataTable datos = _dalReportes.EjecutarReporte("SP_Rep7_Productividad_Ventas_Colaboradores", parametros);
            ViewBag.NombreReporte = "7. Productividad de Ventas por Rol y Colaborador";
            return View("VisorReporte", datos);
        }

        // REPORTE 8: Auditoría de Devoluciones y Mermas
        public ActionResult Reporte8(int? idCliente, int? idProducto, int? idCategoria, int? idColaborador, DateTime? fechaDevolucionDesde)
        {
            var parametros = new SqlParameter[]
            {
                new SqlParameter("@Id_Cliente", (object)idCliente ?? DBNull.Value),
                new SqlParameter("@Id_Producto", (object)idProducto ?? DBNull.Value),
                new SqlParameter("@Id_Categoria", (object)idCategoria ?? DBNull.Value),
                new SqlParameter("@Id_Colaborador", (object)idColaborador ?? DBNull.Value),
                new SqlParameter("@Fecha_Devolucion_Desde", (object)fechaDevolucionDesde ?? DBNull.Value)
            };

            DataTable datos = _dalReportes.EjecutarReporte("SP_Rep8_Auditoria_Devoluciones_Garantias", parametros);
            ViewBag.NombreReporte = "8. Auditoría de Devoluciones y Mermas";
            return View("VisorReporte", datos);
        }
    }
}