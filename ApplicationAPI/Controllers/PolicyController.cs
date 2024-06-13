using ApplicationAPI.DTOs;
using ApplicationAPI.Errors;
using AutoMapper;
using DomainModule.Entities;
using DomainModule.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PolicyController : ControllerBase
    {
        private readonly IInsurancePolicyRepository _repository;
        private readonly IMapper _mapper;
        public PolicyController(IInsurancePolicyRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IReadOnlyList<InsurancePolicyRequestDto>>> GetInsurancePoliciesByUserId(int id)
        {
            var policy = await _repository.GetInsurancePoliciesByUserId(id);

            if(policy == null) return NotFound(new ApiError(404));

            return Ok(_mapper.Map<IReadOnlyList<InsurancePolicy>, IReadOnlyList<InsurancePolicyRequestDto>>(policy));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InsurancePolicyRequestDto>> GetInsurancePolicyById(int id)
        {
            var policy = await _repository.GetInsurancePolicyById(id);

            if(policy == null) return NotFound(new ApiError(404));

            return Ok(_mapper.Map<InsurancePolicy, InsurancePolicyRequestDto>(policy));
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteInsurancePolicyById(int id)
        {
            if(await _repository.DeleteInsurancePolicyById(id)){
                return Ok();
            } else {
                return BadRequest(new ApiError(400));
            }
        }
        
    
        [HttpPut]
        public async Task<ActionResult> UpdateInsurancePolicy(InsurancePolicyUpdateDto dto)
        {
            var insurancePolicy = new InsurancePolicy {
                Id = dto.Id,
                PolicyNumber = dto.PolicyNumber,
                InsuranceAmount = dto.InsuranceAmount,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };
    
            if(await _repository.UpdateInsurancePolicy(insurancePolicy)){
                return Ok();
            } else {
                return BadRequest(new ApiError(400));
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddInsurancePolicy(InsurancePolicyAddDto dto)
        {
            var insurancePolicy = new InsurancePolicy {
                PolicyNumber = dto.PolicyNumber,
                InsuranceAmount = dto.InsuranceAmount,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                UserId = dto.UserId
            };

            if(await _repository.AddInsurancePolicy(insurancePolicy)){
                // TODO - create headers
                return Created();
            }
            else
            {
                return BadRequest(new ApiError(400));
            }
        }
    }
}