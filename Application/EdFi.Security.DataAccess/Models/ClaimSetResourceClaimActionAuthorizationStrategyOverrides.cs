﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EdFi.Security.DataAccess.Models
{
    public class ClaimSetResourceClaimActionAuthorizationStrategyOverrides
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClaimSetResourceClaimActionAuthorizationStrategyOverrideId { get; set; }

        public int ClaimSetResourceClaimActionAuthorizationId { get; set; }

        [Required]
        [Index(IsUnique = true, Order = 1)]
        [ForeignKey("ClaimSetResourceClaimActionAuthorizationId")]
        public ClaimSetResourceClaimActionAuthorizations ClaimSetResourceClaimActionAuthorization { get; set; }

        [Required]
        [Index(IsUnique = true, Order = 2)]
        public AuthorizationStrategy AuthorizationStrategy { get; set; }
    }
}