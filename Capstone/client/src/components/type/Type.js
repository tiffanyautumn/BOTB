import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { deleteType } from "../../modules/typeManager";
import { TypeEdit } from "./TypeEdit";


export const Type = ({ type, typeDelete, getTypes, typeEdit }) => {

    const [editForm, setEditForm] = useState(false)



    const deleteButton = () => {
        return deleteType(type.id)
            .then(() => {
                getTypes()
            })
    }

    return (


        <section className="type-details">
            <p className="Name panel-item">
                <span className="title">{type?.name}
                    {typeDelete ? <button className="btn" onClick={(() => deleteButton())}><i className="fa-solid fa-circle-minus"></i></button> : ""}
                    {typeEdit ? <button className="btn" onClick={(() => setEditForm(!editForm))}><i className="fa-solid fa-eye-dropper"></i></button> : ""}
                </span>
                {editForm ? <TypeEdit Type={type} getTypes={getTypes} setEditForm={setEditForm} /> : ""}

            </p>
        </section>
    )
}