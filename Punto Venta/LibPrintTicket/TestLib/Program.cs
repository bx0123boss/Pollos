using System;
using System.Collections.Generic;
using System.Text;

using LibPrintTicket;

namespace TestLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Ticket ticket = new Ticket();

            ticket.AddHeaderLine("ChafiTienda");
            ticket.AddHeaderLine("EXPEDIDO EN:");
            ticket.AddHeaderLine("CALLE CONOCIDA");
            ticket.AddHeaderLine("PUEBLA, PUEBLA");
            ticket.AddHeaderLine("RFC: CSI-020226-MV4");

            ticket.AddSubHeaderLine("Ticket # 1");
            ticket.AddSubHeaderLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
            
            ticket.AddItem("Cantidad", "Nombre producto", "Total");

            ticket.AddTotal("SUBTOTAL", "12.00");
            ticket.AddTotal("IVA", "0");
            ticket.AddTotal("TOTAL", "24");
            ticket.AddTotal("", "");
            ticket.AddTotal("RECIBIDO", "0");
            ticket.AddTotal("CAMBIO", "0");
            ticket.AddTotal("", "");

            ticket.AddFooterLine("VUELVA PRONTO");

            ticket.PrintTicket("EPSON TM-T88III Receipt"); //Nombre de la impresora de tickets
        }
    }
}
