import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { Button, Table } from "reactstrap"
import { getAllTypes } from "../../modules/typeManager"
import { Type } from "./Type"

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
        <div className="container">

            <p className="Name panel-item">
                <span className="title">Types<button onClick={(() => navigate('/type/create'))} className="btn"><i className="fa-solid fa-plus"></i></button>
                    {
                        isAdmin &&
                        <>
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
    )
}