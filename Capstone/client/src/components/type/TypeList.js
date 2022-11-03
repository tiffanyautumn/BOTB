import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Table } from "reactstrap"
import { getAllTypes } from "../../modules/typeManager"
import { Type } from "./Type"
import './Type.css'

export const TypeList = ({ isAdmin, isApproved, searchTermState }) => {
    const [types, setTypes] = useState([])
    const [typeDelete, setTypeDelete] = useState(false)
    const [typeEdit, setTypeEdit] = useState(false)
    const navigate = useNavigate()

    const getTypes = () => {
        getAllTypes().then(t => setTypes(t))
    }

    useEffect(() => {
        getTypes();
    }, []);


    return (
        <div className="type-container">
            <div className="container">

                <p className="type-header panel-item">
                    <span className="title">Types
                        {
                            isAdmin &&
                            <>
                                <button onClick={(() => navigate('/type/create'))} className="btn"><i className="fa-solid fa-plus"></i></button>
                                <button onClick={(() => setTypeDelete(!typeDelete))} className="btn"><i className="fa-solid fa-trash-can"></i></button>
                                <button onClick={(() => setTypeEdit(!typeEdit))} className="btn"><i className="fa-solid fa-eye-dropper"></i></button>
                            </>
                        }

                    </span>
                </p>
                <div className="row ">

                    {types.map((type) => (
                        <Type type={type} key={type.id} isAdmin={isAdmin} typeDelete={typeDelete} getTypes={getTypes} typeEdit={typeEdit} />
                    ))}


                </div>



            </div>
        </div>
    )
}