using System.Collections.Generic;
using System.Threading.Tasks;
using TrackingManagement.DTOs;
using TrackingManagement.Models;

namespace TrackingManagement.Repositories
{
    public interface ISegmentationService
    {
        public Task<PagedResponse<List<Segmentation>>> getSegmentations(PaginationFilter paginationFilter, SegmentationFilter segmentationFilter, List<int> unitIds);
        public int createSegmentation(Segmentation segmentation);
        public int updateSegmentation(UpdateSegmentation updateSegmentation, int Id);
        public int updateSegmentationRoute(UpdateSegmentationRoute updateSegmentationRoute);
    }
}
