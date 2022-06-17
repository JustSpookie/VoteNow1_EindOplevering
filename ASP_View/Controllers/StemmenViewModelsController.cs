using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_View.Data;
using ASP_View.Models;
using BusinessLogic;
using DataAccessLayer;
using System.Data;

namespace ASP_View.Controllers
{
    public class StemmenViewModelsController : Controller
    {
        VerkiezingContainer verkiezingContainer = new VerkiezingContainer(new VerkiezingDAL());
        KandidaatContainer kandidaatContainer = new KandidaatContainer(new KandidaatDAL());
        StemContainer StemContainer = new StemContainer(new StemDAL());
        UserModel usermodel = new UserModel();

        public StemmenViewModelsController(ASP_ViewContext context)
        {
            
        }

        // GET: StemmenViewModels
        public IActionResult Index()
        {
            if ((int)HttpContext.Session.GetInt32("UserID") == 0) { return RedirectToAction("Index", "Home"); }

            DataTable dataTable = new DataTable();
            List<Verkiezing> verkiezingList = verkiezingContainer.GetAllVerkiezingenVote();

            dataTable.Columns.Add("VerkiezingID");
            dataTable.Columns.Add("VerkiezingNaam");
            dataTable.Columns.Add("mogelijk");
            dataTable.Columns.Add("status");

            foreach (Verkiezing verkiezing in verkiezingList)
            {
                if(verkiezing.UserID == (int)HttpContext.Session.GetInt32("UserID"))
                {
                    dataTable.Rows.Add(verkiezing.VerkiezingID, verkiezing.VerkiezingNaam, 1, "Eigen verkiezing");
                }
                else if(StemContainer.CheckStem(verkiezing.VerkiezingID, (int)HttpContext.Session.GetInt32("UserID")))
                {
                    dataTable.Rows.Add(verkiezing.VerkiezingID, verkiezing.VerkiezingNaam, 1, "All op gestemt");

                }
                else
                {
                    dataTable.Rows.Add(verkiezing.VerkiezingID, verkiezing.VerkiezingNaam, 0,"Open");
                }
            }

            return View(dataTable);
        }

        // GET: StemmenViewModels/Stemmen/5
        public IActionResult Stemmen(int id)
        {
            if ((int)HttpContext.Session.GetInt32("UserID") == 0) { return RedirectToAction("Index", "Home"); }

            StemmenViewModel stemmenViewModel = new StemmenViewModel();

            HttpContext.Session.SetInt32("VerkiezingID", id);

            List<KandidaatViewModel> kandidatenTemp = new List<KandidaatViewModel>();

            foreach(Kandidaat kandidaat in kandidaatContainer.GetKandidatenFromVerkiezing(id))
            {
                kandidatenTemp.Add(new KandidaatViewModel(kandidaat.KandidaatID, kandidaat.KandidaatNaam, kandidaat.Selected));
            }

            stemmenViewModel.kandidaten = kandidatenTemp;

            Verkiezing verkiezing = verkiezingContainer.GetVerkiezing(id);
            stemmenViewModel.verkiezing = new VerkiezingViewModel(verkiezing.VerkiezingID, verkiezing.VerkiezingNaam);

            return View(stemmenViewModel);
        }

        // POST: StemmenViewModels/Stemmen/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Stemmen(StemmenViewModel stemmenViewModel)
        {
            List<Kandidaat> kandidatenTemp = kandidaatContainer.GetKandidatenFromVerkiezing((int)HttpContext.Session.GetInt32("VerkiezingID"));
            for (int i = 0; i < stemmenViewModel.kandidaten.Count; i++)
            {
                if (stemmenViewModel.kandidaten[i].Selected == true)
                {
                    StemContainer.ADDStem(stemmenViewModel.verkiezing.VerkiezingID, kandidatenTemp[i].KandidaatID, (int)HttpContext.Session.GetInt32("UserID"));
                }
            }

            return RedirectToAction(nameof(Index));

            return View(stemmenViewModel);
        }

        // GET: StemmenViewModels/Resultaat
        public IActionResult Resultaat(int id)
        {
            if ((int)HttpContext.Session.GetInt32("UserID") == 0) { return RedirectToAction("Index", "Home"); }

            List<Kandidaat> kandidaats = kandidaatContainer.GetKandidatenFromVerkiezing(id);
            ResultaatsModels resultaatsModels = new ResultaatsModels();

            foreach(Kandidaat kandidaat in kandidaats)
            {
                int stemcount = StemContainer.GetStemCount(kandidaat.KandidaatID, id);
                resultaatsModels.resultaatModels.Add(new ResultaatModel(new KandidaatViewModel(kandidaat.KandidaatID, kandidaat.KandidaatNaam), stemcount));
            }

            return View(resultaatsModels);
        }

        // POST: StemmenViewModels/Resultaat
        [HttpPost, ActionName("Resultaat")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {

            

            return RedirectToAction(nameof(Index));
        }

        
    }
}
