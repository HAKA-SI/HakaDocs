export interface Customer {
    id: number;
    insertDate: string;
    updateDate: string;
    active: boolean;
    haKaDocClient: string;
    haKaDocClientId: number;
    idNum: string;
    firstName: string;
    lastName: string;
    gender: number | null;
    phoneNumber: string;
    countryCode: string;
    customerCode: string;
    secondPhoneNumber: string;
    dateOfBirth: string | null;
    created: string;
    countryId: number | null;
    country: string;
    cityId: number | null;
    city: string;
    districtId: number | null;
    district: string;
    birthCountryId: number | null;
    birthCountry: string;
    birthCityId: number | null;
    birthCity: string;
    birthDistrictId: number | null;
    birthDistrict: string;
    maritalStatusId: number | null;
    maritalSatus: string;
    tempData: number;
    validated: boolean;
    toBeValidatedEmail: string;
    accountDataValidated: boolean;
    postalBox: string;
    nationalIDNum: string;
    insertUserId: number;
}