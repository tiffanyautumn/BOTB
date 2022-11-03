import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Table } from "reactstrap"
import { getAllBrands } from "../../modules/brandManager"
import { Brand } from "./Brand"
import './Brand.css'
export const BrandList = ({ isAdmin, isApproved, searchTermState }) => {
    const [brands, setBrands] = useState([])
    const [brandDelete, setBrandDelete] = useState(false)
    const [brandEdit, setBrandEdit] = useState(false)
    const navigate = useNavigate()

    const getBrands = () => {
        getAllBrands().then(b => setBrands(b))
    }

    useEffect(() => {
        getBrands();
    }, []);


    return (
        <div className="container">

            <p className="brand-header panel-item">
                <span className="title">Brands
                    {
                        isAdmin &&
                        <>
                            <button onClick={(() => navigate('/brand/create'))} className="btn"><i className="fa-solid fa-plus"></i></button>
                            <button onClick={(() => setBrandDelete(!brandDelete))} className="btn"><i className="fa-solid fa-trash-can"></i></button>
                            <button onClick={(() => setBrandEdit(!brandEdit))} className="btn"><i className="fa-solid fa-eye-dropper"></i></button>
                        </>
                    }

                </span>
            </p>
            <div className="row ">

                {brands.map((brand) => (
                    <Brand brand={brand} key={brand.id} isAdmin={isAdmin} brandDelete={brandDelete} getBrands={getBrands} brandEdit={brandEdit} />
                ))}


            </div>



        </div>
    )
}