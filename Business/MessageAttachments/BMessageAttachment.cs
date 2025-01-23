using Data;
using Microsoft.Extensions.Configuration;
using Service.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.MessageAttachments
{
    public class BMessageAttachment(DreamVocabBoxContext db, IConfiguration configuration, IUserRepositoryService userRepositoryService) : BaseBusiness(db, configuration, userRepositoryService)
    {

    }
}
