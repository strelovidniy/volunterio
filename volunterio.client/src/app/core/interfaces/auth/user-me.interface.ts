import IRole from '../role/role.interface';
import IUserDetails from '../user/user-details.interface';


interface IUserMe {
    access: IRole;
    email: string;
    firstName: string;
    id: string;
    lastName: string;
    details: IUserDetails;
}

export default IUserMe;
