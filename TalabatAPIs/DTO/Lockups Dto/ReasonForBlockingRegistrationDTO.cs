﻿namespace Grad.APIs.DTO.Lockups_Dto
{
    public class ReasonForBlockingRegistrationDTO
    {

        public int Id { get; set; }

        public string TheReasonForBlockingRegistration { get; set; }
        public int? UniversityId { get; set; }


        public string University { get; set; }
    }
}
