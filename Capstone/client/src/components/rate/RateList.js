import { useEffect, useState } from "react"
import { useNavigate } from "react-router-dom"
import { getAllRates } from "../../modules/roleManager"
import { Rate } from "./Rate"
import './Rate.css'
export const RateList = ({ isAdmin, isApproved, searchTermState }) => {
    const [rates, setRates] = useState([])
    const [rateDelete, setRateDelete] = useState(false)
    const [rateEdit, setRateEdit] = useState(false)
    const navigate = useNavigate()

    const getRates = () => {
        getAllRates().then(r => setRates(r))
    }

    useEffect(() => {
        getRates();
    }, []);


    return (
        <div className="container">

            <p className="rate-header panel-item">
                <span className="title">Ratings
                    {
                        isAdmin &&
                        <>
                            <button onClick={(() => navigate('/rate/create'))} className="btn"><i className="fa-solid fa-plus"></i></button>
                            <button onClick={(() => setRateDelete(!rateDelete))} className="btn"><i className="fa-solid fa-trash-can"></i></button>
                            <button onClick={(() => setRateEdit(!rateEdit))} className="btn"><i className="fa-solid fa-eye-dropper"></i></button>
                        </>
                    }

                </span>
            </p>
            <div className="row ">

                {rates.map((r) => (
                    <Rate rate={r} key={r.id} isAdmin={isAdmin} rateDelete={rateDelete} getRates={getRates} rateEdit={rateEdit} />
                ))}


            </div>



        </div>
    )
}