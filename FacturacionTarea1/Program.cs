using FacturacionTarea1.Entities;
using FacturacionTarea1.Services;

ArticuloService articuloservice = new ArticuloService();
FacturaService facturaService = new FacturaService();
FormaPagoService formaPagoService = new FormaPagoService();

using (UnitOfWork ufw = new UnitOfWork())