import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { deleteHazard } from "../../modules/hazardManager";
import { HazardEdit } from "./HazardEdit";


export const Hazards = ({ hazard, getHazards, hazardDelete, hazardEdit }) => {
    const [editForm, setEditForm] = useState(false)



    const deleteButton = () => {
        return deleteHazard(hazard.id)
            .then(() => {
                getHazards()
            })
    }

    return (

        <section className="hazard-details">
            <p className="Name panel-item">
                <span className="title">{hazard?.name}
                    {hazardDelete ? <button className="btn" onClick={(() => deleteButton())}><i className="fa-solid fa-circle-minus"></i></button> : ""}
                    {hazardEdit ? <button className="btn" onClick={(() => setEditForm(!editForm))}><i className="fa-solid fa-eye-dropper"></i></button> : ""}
                </span>
                {hazard?.description}
                {editForm ? <HazardEdit Hazard={hazard} getHazards={getHazards} setEditForm={setEditForm} /> : ""}

            </p>
        </section>
    )
}