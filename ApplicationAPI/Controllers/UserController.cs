using Microsoft.AspNetCore.Mvc;
using DomainModule.Interfaces;
using DomainModule.Entities;
using AutoMapper;
using ApplicationAPI.DTOs;
using ApplicationAPI.Errors;

namespace ApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<UserRequestDto>>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            return Ok(_mapper.Map<IReadOnlyList<User>, IReadOnlyList<UserRequestDto>>(users));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserRequestDto>> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null) return NotFound(new ApiError(404));
            return _mapper.Map<User, UserRequestDto>(user);
        }

        [HttpPost]
        public async Task<ActionResult> AddUser(UserAddDto dto)
        {

            var userToAdd = new User
            {
                Name = dto.Name,
                Email = dto.Email
            };

            if (await _userRepository.AddUser(userToAdd))
            {
                // TODO - create headers
                return Created();
            }
            else
            {
                return BadRequest(new ApiError(400));
            }
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UserUpdateDto dto)
        {

            var user = new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email
            };

            if (await _userRepository.UpdateUser(user))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiError(400));
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserById(int id)
        {
            if (await _userRepository.DeleteUserById(id))
            {
                return Ok();
            }
            else
            {
                return BadRequest(new ApiError(400));
            }
        }
    }
}