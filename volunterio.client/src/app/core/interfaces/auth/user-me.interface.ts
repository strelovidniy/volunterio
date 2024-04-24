import IRole from '../role/role.interface';


interface IUserMe {
    access: IRole;
    email: string;
    firstName: string;
    id: string;
    lastName: string;
}

export default IUserMe;
