using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Areas.Manager.Controllers
{
    [Authorize(Roles = "DataManager")]
    [Area("Manager")]
    public abstract class ManagerController : Controller
    {
    }
}
