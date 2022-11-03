import React, { useState } from "react";
import { deleteRate } from "../../modules/rateManager";
import { RateEdit } from "./RateEdit";


export const Rate = ({ rate, rateDelete, getRates, rateEdit }) => {

    const [editForm, setEditForm] = useState(false)



    const deleteButton = () => {
        return deleteRate(rate.id)
            .then(() => {
                getRates()
            })
    }

    return (


        <section className="rate-details">
            <p className="Name panel-item">
                <span className="title">{rate?.rating}
                    {rateDelete ? <button className="btn" onClick={(() => deleteButton())}><i className="fa-solid fa-circle-minus"></i></button> : ""}
                    {rateEdit ? <button className="btn" onClick={(() => setEditForm(!editForm))}><i className="fa-solid fa-eye-dropper"></i></button> : ""}
                </span>
                {editForm ? <RateEdit Rate={rate} getRates={getRates} setEditForm={setEditForm} /> : ""}

            </p>
        </section>
    )
}