using System.ComponentModel.DataAnnotations;

namespace RACB.APIs.DTOs
{
    public class UpdatedCourseDto : CourseModificationBaseDto
    {
        [Required]
        public override string Description
        {
            get => base.Description;
            set => base.Description = value;
        }
    }
}
