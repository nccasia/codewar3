<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;
use Illuminate\Http\Exceptions\HttpResponseException;
use Illuminate\Http\JsonResponse;
use Illuminate\Validation\ValidationException;
use Illuminate\Contracts\Validation\Validator;

class ContactRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     *
     * @return bool
     */
    public function authorize()
    {
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array
     */
    public function rules()
    {
        return [
            'first_name' => 'required',
            'second_name' => 'required',
            'know_email' => 'email',
            'linked_in_url' => 'required',
            'organization_id' => 'required|exists:organizations,id',
            // 'owner_user_id' => 'required|exists:users,user_id',
        ];
    }

    public function messages()
    {
        return [
            // 'name.required' => 'A name for the contact is required.',
            'linked_in_url.required' => 'linked_in_url for the contact is required.',
            'know_email.email' => 'Email for the contact is invalid.',
        ];
    }
    public function failedValidation(Validator $validator)
    {
        $errors = (new ValidationException($validator))->errors();
        throw new HttpResponseException(response()->json(
            [
                'error' => $errors,
                'status_code' => 422,
            ],
            JsonResponse::HTTP_UNPROCESSABLE_ENTITY
        ));
    }
}
